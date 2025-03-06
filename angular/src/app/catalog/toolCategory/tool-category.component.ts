import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { ToolCategoriesService, ToolCategoryDto, ToolCategoryInListDto } from '@proxy/catalog/tool-categories';
import { ToolCategoryDetailComponent } from './tool-category-detail.component';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-tool-category',
  templateUrl: './tool-category.component.html',
  styleUrls: ['./tool-category.component.scss'],
})
export class ToolCategoryComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: ToolCategoryInListDto[] = [];
  public selectedItems: ToolCategoryInListDto[] = [];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  //Filter
  attributecategories: any[] = [];
  keyword: string = '';
  categoryId: string = '';

  constructor(
    private toolCategoryService: ToolCategoriesService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.toolCategoryService
      .getListFilter({
        keyword: this.keyword,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<ToolCategoryInListDto>) => {
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
    const ref = this.dialogService.open(ToolCategoryDetailComponent, {
      header: 'Thêm mới danh mục công cụ',
      width: '70%',
    });

    ref.onClose.subscribe((data: ToolCategoryDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Thêm danh mục công cụ thành công');
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
    const ref = this.dialogService.open(ToolCategoryDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật danh mục công cụ',
      width: '70%',
    });

    ref.onClose.subscribe((data: ToolCategoryDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Cập nhật danh mục công cụ thành công');
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
    this.toolCategoryService
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

  visibilityChange(id: string, e: { checked: boolean }) {
    this.toolCategoryService
      .updateVisibility(id, e.checked)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.selectedItems = [];
          this.notificationService.showSuccess('Cập nhật hiện thị thành công');
        },
        error: () => {},
      });
  }
}
