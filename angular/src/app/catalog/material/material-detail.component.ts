import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { UtilityService } from '../../shared/services/utility.service';
import { NotificationService } from '../../shared/services/notification.service';
import { DomSanitizer } from '@angular/platform-browser';
import { MaterialsService, MaterialDto, MaterialInListDto } from '@proxy/catalog/materials';
import { materialTypeOptions } from '@proxy/hcn/materials';
import { MaterialCategoriesService, MaterialCategoryInListDto } from '@proxy/catalog/material-categories';

@Component({
  selector: 'app-material-detail',
  templateUrl: './material-detail.component.html',
})
export class MaterialDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  btnDisabled = false;
  blockedPanel: boolean = false;
  public form: FormGroup;
  public thumbnailImage;

  //Dropdown
  materialCategories: any[] = [];
  materialParents: any[] = [];
  selectedEntity = {} as MaterialDto;
  materialTypes: any[] = [];
  constructor(
    private materialService: MaterialsService,
    private materialCategoryService: MaterialCategoriesService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private utilService: UtilityService,
    private notificationService: NotificationService,
    private cd: ChangeDetectorRef,
    private sanitizer: DomSanitizer
  ) {}

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.buildForm();
    this.loadMaterialTypes();
    this.initFormData();
  }

  initFormData() {
    var materialCategories = this.materialCategoryService.getListAll();
    var materialParents = this.materialService.getListAll();
    forkJoin({
      materialCategories,
      materialParents
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          //Push data to dropdown
          var materialCategorieResults = response.materialCategories as MaterialCategoryInListDto[];
          materialCategorieResults.forEach(element => {
            this.materialCategories.push({
              value: element.id,
              label: element.name,
            });
          });

          var materialParentResults = response.materialParents as MaterialInListDto[];
          materialParentResults.forEach(element => {
            this.materialParents.push({
              value: element.id,
              label: element.name,
            });
          });

          //Load edit data to form
          if (this.utilService.isEmpty(this.config.data?.id)) {
            this.getNewSuggestionCode();
            this.toggleBlockUI(false);
          } else {
            this.loadFormDetails(this.config.data?.id);
          }
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.materialService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: MaterialDto) => {
          this.selectedEntity = response;
          this.loadThumbnail(this.selectedEntity.thumbnailPicture);
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.toggleBlockUI(true);

    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.materialService
        .create(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);
            this.ref.close(this.form.value);
          },
          error: err => {
            this.notificationService.showError(err.error.error.message);
            this.toggleBlockUI(false);
          },
        });
    } else {
      this.materialService
        .update(this.config.data?.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);
            this.ref.close(this.form.value);
          },
          error: err => {
            this.notificationService.showError(err.error.error.message);
            this.toggleBlockUI(false);
          },
        });
    }
  }

  private buildForm() {
    this.form = this.fb.group({
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([Validators.required, Validators.maxLength(256)])
      ),
      slug: new FormControl(
        this.selectedEntity.slug || null,
        Validators.compose([Validators.required, Validators.maxLength(256)])
      ),
      code: new FormControl(
        this.selectedEntity.code || null,
        Validators.compose([Validators.required, Validators.maxLength(128)])
      ),
      categoryId: new FormControl(this.selectedEntity.categoryId || null, Validators.required),
      materialType: new FormControl(this.selectedEntity.materialType || null, Validators.required),
      description: new FormControl(this.selectedEntity.description || null),
      pictures: new FormControl(this.selectedEntity.pictures || null, Validators.maxLength(512)),
      visibility: new FormControl(this.selectedEntity.visibility || false),
      keywordSEO: new FormControl(this.selectedEntity.keywordSEO || null, Validators.maxLength(512)),
      descriptionSEO: new FormControl(this.selectedEntity.descriptionSEO || null, Validators.maxLength(1024)),
      parentId: new FormControl(this.selectedEntity.parentId || null),
      coverPictureName: new FormControl(this.selectedEntity.thumbnailPicture || null),
      coverPictureContent: new FormControl(null),
    });
  }

  validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên hiện thị' },
      { type: 'maxLength', message: 'Bạn không được nhập quá 256 kí tự' },
    ],
    slug: [
      { type: 'required', message: 'Bạn phải nhập slug' },
      { type: 'maxLength', message: 'Bạn không được nhập quá 256 kí tự' },
    ],
    code: [
      { type: 'required', message: 'Bạn phải nhập mã' },
      { type: 'maxLength', message: 'Bạn không được nhập quá 128 kí tự' },
    ],
    categoryId: [{ type: 'required', message: 'Bạn phải chọn danh mục nguyên liệu' }],
    materialType: [{ type: 'required', message: 'Bạn phải chọn loại nguyên liệu' }],
    pictures: [{ type: 'maxLength', message: 'Bạn không được nhập quá 1024 kí tự' }],
    keywordSEO: [{ type: 'maxLength', message: 'Bạn không được nhập quá 512 kí tự' }],
    descriptionSEO: [{ type: 'maxLength', message: 'Bạn không được nhập quá 1024 kí tự' }],
  };

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
      this.btnDisabled = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
        this.btnDisabled = false;
      }, 1000);
    }
  }

  onFileChange(event){
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.form.patchValue({
          coverPictureName: file.name,
          coverPictureContent: reader.result,
        });
        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }

  generateSlug() {
    this.form.controls['slug'].setValue(this.utilService.MakeSeoTitle(this.form.get('name').value));
  }

  loadThumbnail(fileName: string){
    this.materialService.getThumbnailImage(fileName)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: string) => {
        var fileExt = this.selectedEntity.thumbnailPicture?.split('.').pop();
        this.thumbnailImage = this.sanitizer.bypassSecurityTrustResourceUrl(
          `data:image/${fileExt};base64, ${response}`
        );
      },
    });
  }

  getNewSuggestionCode(){
    this.materialService.getSuggestNewCode()
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: string)=>{
        this.form.patchValue({
          code: response
        })
      }
    })
  }

  loadMaterialTypes() {
    materialTypeOptions.forEach(element => {
      this.materialTypes.push({
        value: element.value,
        label: element.key,
      });
    });
  }
}
