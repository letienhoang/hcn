import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { MaterialsService, MaterialDto, MaterialInListDto } from '@proxy/catalog/materials';
import { MaterialDetailComponent } from './material-detail.component';
import { MaterialTagComponent } from './material-tag.component';
import { MaterialCategoriesService, MaterialCategoryInListDto } from '@proxy/catalog/material-categories';

@Component({
  selector: 'app-material',
  templateUrl: './material.component.html',
  styleUrls: ['./material.component.scss'],
})
export class MaterialComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: MaterialInListDto[] = [];
  public selectedItems: MaterialInListDto[] = [];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  //Filter
  categories: any[] = [];
  keyword: string = '';
  categoryId: string = '';

  constructor(
    private materialService: MaterialsService,
    private categoryService: MaterialCategoriesService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadCategories();
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.materialService
      .getListFilter({
        keyword: this.keyword,
        categoryId: this.categoryId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<MaterialInListDto>) => {
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
    const ref = this.dialogService.open(MaterialDetailComponent, {
      header: 'Thêm mới nguyên liệu',
      width: '70%',
    });

    ref.onClose.subscribe((data: MaterialDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Thêm nguyên liệu thành công');
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
    const ref = this.dialogService.open(MaterialDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật nguyên liệu',
      width: '70%',
    });

    ref.onClose.subscribe((data: MaterialDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Cập nhật nguyên liệu thành công');
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
    this.materialService
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

  loadCategories() {
    this.categoryService.getListAll().subscribe((response: MaterialCategoryInListDto[]) => {
      response.forEach(element => {
        this.categories.push({
          value: element.id,
          label: element.name,
        });
      });
    });
  }

  manageMaterialTag(id: string){
    const ref = this.dialogService.open(MaterialTagComponent, {
      data: {
        id: id,
      },
      header: 'Quản lý thẻ nguyên liệu',
      width: '70%',
    });

    ref.onClose.subscribe((data: MaterialDto)=> {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Cập nhật thẻ nguyên liệu thành công');
        this.selectedItems = [];
      }
    });
  }

  visibilityChange(id: string, e: { checked: boolean }) {
    this.materialService
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
