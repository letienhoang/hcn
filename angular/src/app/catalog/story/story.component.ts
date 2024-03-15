import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { StoriesService, StoryDto, StoryInListDto } from '@proxy/catalog/stories';
import { StoryDetailComponent } from './story-detail.component';
import { TopicInListDto, TopicsService } from '@proxy/catalog/topics';
import { StoryTagComponent } from './story-tag.component';
import { TagInListDto } from '@proxy/catalog/tags';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
  styleUrls: ['./story.component.scss'],
})
export class StoryComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: StoryInListDto[] = [];
  public selectedItems: StoryInListDto[] = [];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  //Filter
  topics: any[] = [];
  keyword: string = '';
  topicId: string = '';

  constructor(
    private storyService: StoriesService,
    private topicService: TopicsService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadTopics();
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.storyService
      .getListFilter({
        keyword: this.keyword,
        topicId: this.topicId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<StoryInListDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  showAddModal() {
    const ref = this.dialogService.open(StoryDetailComponent, {
      header: 'Thêm mới câu chuyện',
      width: '70%',
    });

    ref.onClose.subscribe((data: StoryDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Thêm câu chuyện thành công');
        this.selectedItems = [];
      }
    });
  }

  showEditModal() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('Bạn phải chọn một bản ghi');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(StoryDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật câu chuyện',
      width: '70%',
    });

    ref.onClose.subscribe((data: StoryDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Cập nhật câu chuyện thành công');
        this.selectedItems = [];
      }
    });
  }

  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('Phải chọn ít nhất một bản ghi');
      return;
    }
    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message: 'Bạn có chắc muốn xóa bản ghi này?',
      accept: () => {
        this.deleteItemsConfirmed(ids);
      },
    });
  }

  deleteItemsConfirmed(ids: string[]) {
    this.toggleBlockUI(true);
    this.storyService
      .deleteMultiple(ids)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess('Xóa thành công');
          this.loadData();
          this.selectedItems = [];
          this.toggleBlockUI(false);
        },
        error:()=>{
          this.toggleBlockUI(false);
        }
      });
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }

  loadTopics() {
    this.topicService.getListAll().subscribe((response: TopicInListDto[]) => {
      response.forEach(element => {
        this.topics.push({
          value: element.id,
          label: element.name,
        });
      });
    });
  }

  manageStoryTag(id: string){
    const ref = this.dialogService.open(StoryTagComponent, {
      data: {
        id: id,
      },
      header: 'Quản lý thẻ câu chuyện',
      width: '70%',
    });

    ref.onClose.subscribe((data: StoryDto)=> {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Cập nhật thẻ câu chuyện thành công');
        this.selectedItems = [];
      }
    });
  }
}
