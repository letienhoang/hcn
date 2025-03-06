import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { UtilityService } from '../../shared/services/utility.service';
import { NotificationService } from '../../shared/services/notification.service';
import { DomSanitizer } from '@angular/platform-browser';
import { FormulasService, FormulaDto, FormulaInListDto } from '@proxy/catalog/formulas';
import { formulaTypeOptions, levelOptions } from '@proxy/hcn/formulas';
import {
  FormulaCategoriesService,
  FormulaCategoryInListDto,
} from '@proxy/catalog/formula-categories';

@Component({
  selector: 'app-formula-detail',
  templateUrl: './formula-detail.component.html',
})
export class FormulaDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  btnDisabled = false;
  blockedPanel: boolean = false;
  public form: FormGroup;
  public thumbnailImage;

  //Dropdown
  formulaCategories: any[] = [];
  formulaParents: any[] = [];
  selectedEntity = {} as FormulaDto;
  formulaTypes: any[] = [];
  levels: any[] = [];
  constructor(
    private formulaService: FormulasService,
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
    this.loadFormulaTypes();
    this.loadLevels();
    this.initFormData();
  }

  initFormData() {
    var formulaCategories = this.formulaCategoryService.getListAll();
    var formulaParents = this.formulaService.getListAll();
    forkJoin({
      formulaCategories,
      formulaParents,
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          //Push data to dropdown
          var formulaCategorieResults = response.formulaCategories as FormulaCategoryInListDto[];
          formulaCategorieResults.forEach(element => {
            this.formulaCategories.push({
              value: element.id,
              label: element.name,
            });
          });

          var formulaParentResults = response.formulaParents as FormulaInListDto[];
          formulaParentResults.forEach(element => {
            this.formulaParents.push({
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
    this.formulaService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: FormulaDto) => {
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
    if (this.form.valid) {
      this.toggleBlockUI(true);

      if (this.utilService.isEmpty(this.config.data?.id)) {
        this.formulaService
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
        this.formulaService
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
      level: new FormControl(this.selectedEntity.level || null, Validators.required),
      formulaType: new FormControl(this.selectedEntity.formulaType || null, Validators.required),
      executionTime: new FormControl(
        this.selectedEntity.executionTime || null,
        Validators.required
      ),
      briefContent: new FormControl(
        this.selectedEntity.briefContent || null,
        Validators.maxLength(1024)
      ),
      description: new FormControl(this.selectedEntity.description || null),
      liked: new FormControl(this.selectedEntity.liked || 1, Validators.required),
      viewCount: new FormControl(this.selectedEntity.viewCount || 1, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || 1, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || false),
      videoUrl: new FormControl(this.selectedEntity.videoUrl || null, Validators.maxLength(512)),
      referenceSource: new FormControl(
        this.selectedEntity.referenceSource || null,
        Validators.maxLength(512)
      ),
      keywordSEO: new FormControl(
        this.selectedEntity.keywordSEO || null,
        Validators.maxLength(512)
      ),
      descriptionSEO: new FormControl(
        this.selectedEntity.descriptionSEO || null,
        Validators.maxLength(1024)
      ),
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
    categoryId: [{ type: 'required', message: 'Bạn phải chọn danh mục công thức' }],
    level: [{ type: 'required', message: 'Bạn phải chọn cấp độ thực hiện công thức' }],
    formulaType: [{ type: 'required', message: 'Bạn phải chọn loại công thức' }],
    executionTime: [{ type: 'required', message: 'Bạn phải nhập thời gian thực hiện công thức' }],
    briefContent: [{ type: 'maxLength', message: 'Bạn không được nhập quá 1024 kí tự' }],
    liked: [{ type: 'required', message: 'Không để trống số người thích công thức' }],
    viewCount: [{ type: 'required', message: 'Không để trống số người xem công thức' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự công thức' }],
    videoUrl: [{ type: 'maxLength', message: 'Bạn không được nhập quá 512 kí tự' }],
    referenceSource: [{ type: 'maxLength', message: 'Bạn không được nhập quá 512 kí tự' }],
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

  onFileChange(event) {
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

  loadThumbnail(fileName: string) {
    this.formulaService
      .getThumbnailImage(fileName)
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

  getNewSuggestionCode() {
    this.formulaService
      .getSuggestNewCode()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: string) => {
          this.form.patchValue({
            code: response,
          });
        },
      });
  }

  loadFormulaTypes() {
    formulaTypeOptions.forEach(element => {
      this.formulaTypes.push({
        value: element.value,
        label: element.key,
      });
    });
  }

  loadLevels() {
    levelOptions.forEach(element => {
      this.levels.push({
        value: element.value,
        label: element.key,
      });
    });
  }
}
