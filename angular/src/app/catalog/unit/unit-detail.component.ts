import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from '../../shared/services/utility.service';
import { NotificationService } from '../../shared/services/notification.service';
import { UnitsService, UnitDto} from '@proxy/catalog/units';
import { unitTypeOptions } from '@proxy/hcn/units';

@Component({
  selector: 'app-unit-detail',
  templateUrl: './unit-detail.component.html',
})
export class UnitDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  btnDisabled = false;
  blockedPanel: boolean = false;
  public form: FormGroup;

  //Dropdowns
  formulaCategories: any[] = [];
  selectedEntity = {} as UnitDto;
  unitTypes: any[] = [];

  constructor(
    private unitService: UnitsService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private utilService: UtilityService,
    private notificationService: NotificationService
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
    this.loadUnitTypes();
    this.initFormData();
  }

  initFormData() {
    //Load edit data to form
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.toggleBlockUI(false);
    } else {
      this.loadFormDetails(this.config.data?.id);
    }
  }

  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.unitService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UnitDto) => {
          this.selectedEntity = response;
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
      this.unitService
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
      this.unitService
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
      unitType: new FormControl(this.selectedEntity.unitType || null, Validators.required),
      briefContent: new FormControl(this.selectedEntity.briefContent || null, Validators.maxLength(1024)),
      visibility: new FormControl(this.selectedEntity.visibility || false),
    });
  }

  validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên hiện thị' },
      { type: 'maxLength', message: 'Bạn không được nhập quá 256 kí tự' },
    ],
    unitType: [{ type: 'required', message: 'Bạn phải chọn loại đơn vị' }],
    briefContent: [{ type: 'maxLength', message: 'Bạn không được nhập quá 1024 kí tự' }],
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

  loadUnitTypes() {
    unitTypeOptions.forEach(element => {
      this.unitTypes.push({
        value: element.value,
        label: element.key,
      });
    });
  }
}
