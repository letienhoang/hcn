import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { UtilityService } from '../../shared/services/utility.service';
import { NotificationService } from '../../shared/services/notification.service';
import { DomSanitizer } from '@angular/platform-browser';
import { StoriesService, StoryDto, StoryInListDto } from '@proxy/catalog/stories';
import { TopicInListDto, TopicsService } from '@proxy/catalog/topics';

@Component({
  selector: 'app-story-detail',
  templateUrl: './story-detail.component.html',
})
export class StoryDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  btnDisabled = false;
  blockedPanel: boolean = false;
  public form: FormGroup;
  public thumbnailImage;

  //Dropdown
  topics: any[] = [];
  selectedEntity = {} as StoryDto;
  constructor(
    private storyService: StoriesService,
    private topicService: TopicsService,
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
    var topics = this.topicService.getListAll();
    forkJoin({
      topics,
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          //Push data to dropdown
          var topics = response.topics as TopicInListDto[];
          topics.forEach(element => {
            this.topics.push({
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
    this.storyService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: StoryDto) => {
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
        this.storyService
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
        this.storyService
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
      title: new FormControl(
        this.selectedEntity.title || null,
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
      briefContent: new FormControl(
        this.selectedEntity.briefContent || null,
        Validators.maxLength(1024)
      ),
      content: new FormControl(this.selectedEntity.content || null),
      pictures: new FormControl(this.selectedEntity.pictures || null, Validators.maxLength(512)),
      liked: new FormControl(this.selectedEntity.liked || 1, Validators.required),
      viewCount: new FormControl(this.selectedEntity.viewCount || 1, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || 1, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || false),
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
      topicId: new FormControl(this.selectedEntity.topicId || null),
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
    briefContent: [{ type: 'maxLength', message: 'Bạn không được nhập quá 1024 kí tự' }],
    pictures: [{ type: 'maxLength', message: 'Bạn không được nhập quá 1024 kí tự' }],
    liked: [{ type: 'required', message: 'Bạn phải nhập lượt thích (ít nhất là 1 lượt)' }],
    viewCount: [{ type: 'required', message: 'Bạn phải nhập lượt xem (ít nhất là 1 lượt)' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự (ít nhất là 1)' }],
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
    this.form.controls['slug'].setValue(
      this.utilService.MakeSeoTitle(this.form.get('title').value)
    );
  }

  loadThumbnail(fileName: string) {
    this.storyService
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
    this.storyService
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
}
