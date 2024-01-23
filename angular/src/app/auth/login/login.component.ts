import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { LoginReqestDto } from 'src/app/shared/models/login-request.dto';
import { LoginResponseDto } from 'src/app/shared/models/login-response.dto';
import { AuthService } from 'src/app/shared/services/auth.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { TokenStorageService } from 'src/app/shared/services/token.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent implements OnDestroy {
    private ngUnsubscribe = new Subject<void>();
  
    valCheck: string[] = ['remember'];
  
    password!: string;
  
    loginForm: FormGroup;
    public blockedPanel: boolean = false;
  
    constructor(
      public layoutService: LayoutService,
      private fb: FormBuilder,
      private authService: AuthService,
      private router: Router,
      private tokenService: TokenStorageService,
      private notificationService: NotificationService
    ) {
      this.loginForm = this.fb.group({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
      });
    }
  
    login() {
      this.toggleBlockUI(true);
      var request: LoginReqestDto = {
        username: this.loginForm.controls['username'].value,
        password: this.loginForm.controls['password'].value,
      };
      this.authService
        .login(request)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: (res: LoginResponseDto) => {
            this.tokenService.saveToken(res.access_token);
            this.tokenService.saveRefreshToken(res.refresh_token);
            this.toggleBlockUI(false);
            this.router.navigate(['']);
          },
          error: () => {
              this.notificationService.showError("Đăng nhập không đúng.");
              this.toggleBlockUI(false);
          },
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
  
    ngOnDestroy(): void {
      this.ngUnsubscribe.next();
      this.ngUnsubscribe.complete();
    }
}
