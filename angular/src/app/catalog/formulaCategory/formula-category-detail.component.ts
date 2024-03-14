import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { UtilityService } from '../../shared/services/utility.service';
import { NotificationService } from '../../shared/services/notification.service';
import { DomSanitizer } from '@angular/platform-browser';
import { FormulaCategoriesService, FormulaCategoryDto, FormulaCategoryInListDto } from '@proxy/catalog/formula-categories';

@Component({
  selector: 'app-formula-category-detail',
  templateUrl: './formula-category-detail.component.html',
})
export class FormulaCategoryDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  btnDisabled = false;
  blockedPanel: boolean = false;
  public form: FormGroup;
  public thumbnailImage;

  //Dropdown
  formulaCategories: any[] = [];
  selectedEntity = {} as FormulaCategoryDto;
  constructor(
    private formulaCategoryService: FormulaCategoriesService,
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
    this.initFormData();
  }

  initFormData() {
    var formulaCategories = this.formulaCategoryService.getListAll();
    forkJoin({
      formulaCategories
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          //Push data to dropdown
          var formulaCategories = response.formulaCategories as FormulaCategoryInListDto[];
          formulaCategories.forEach(element => {
            this.formulaCategories.push({
              value: element.id,
              label: element.name,
            });
          });

          //Load edit data to form
          if (this.utilService.isEmpty(this.config.data?.id)) {
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
    this.formulaCategoryService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: FormulaCategoryDto) => {
          this.selectedEntity = response;
          this.loadThumbnail(this.selectedEntity.coverPicture);
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
      this.formulaCategoryService
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
      this.formulaCategoryService
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
      description: new FormControl(this.selectedEntity.description || null),
      keywordSEO: new FormControl(this.selectedEntity.keywordSEO || null, Validators.maxLength(512)),
      descriptionSEO: new FormControl(this.selectedEntity.descriptionSEO || null, Validators.maxLength(1024)),
      visibility: new FormControl(this.selectedEntity.visibility || false),
      parentId: new FormControl(this.selectedEntity.parentId || null),
      coverPictureName: new FormControl(this.selectedEntity.coverPicture || null),
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
    this.formulaCategoryService.getThumbnailImage(fileName)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: string) => {
        var fileExt = this.selectedEntity.coverPicture?.split('.').pop();
        this.thumbnailImage = this.sanitizer.bypassSecurityTrustResourceUrl(
          `data:image/${fileExt};base64, ${response}`
        );
      },
    });
  }
}
