import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  template: `
    <router-outlet></router-outlet>
  `,
})
export class AppComponent {
  constructor(private primengConfig: PrimeNGConfig) { }

    ngOnInit() {
      this.primengConfig.ripple = true;
      document.documentElement.style.fontSize = '14px';

      // if(this.authService.isAuthenticated() == false){
      //   this.router.navigate([LOGIN_URL]);
      // }
    }
}
