import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService) {}

  ngOnInit() {
    this.model = [
      {
        label: 'Trang chủ',
        items: [{ label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }],
      },
      {
        label: 'Công thức',
        items: [
          {
            label: 'Danh sách công thức',
            icon: 'pi pi-fw pi-clone',
            routerLink: ['/catalog/formula'],
            permission: 'HCNAdminCatalog.Formula',
          },
          {
            label: 'Danh mục công thức',
            icon: 'pi pi-fw pi-clone',
            routerLink: ['/catalog/formula-category'],
            permission: 'HCNAdminCatalog.FormulaCategory',
          },
        ],
      },
      {
        label: 'Nguyên liệu',
        items: [
          {
            label: 'Danh sách nguyên liệu',
            icon: 'pi pi-fw pi-clone',
            routerLink: ['/catalog/material'],
            permission: 'HCNAdminCatalog.Material',
          },
          {
            label: 'Danh mục nguyên liệu',
            icon: 'pi pi-fw pi-clone',
            routerLink: ['/catalog/material-category'],
            permission: 'HCNAdminCatalog.MaterialCategory',
          },
        ],
      },
      {
        label: 'Cài đặt',
        icon: 'pi pi-fw pi-briefcase',
        items: [
          {
            label: 'Tài khoản',
            icon: 'pi pi-fw pi-user',
            items: [
              {
                label: 'Login',
                icon: 'pi pi-fw pi-sign-in',
                routerLink: ['/auth/login'],
              },
              {
                label: 'Error',
                icon: 'pi pi-fw pi-times-circle',
                routerLink: ['/auth/error'],
              },
              {
                label: 'Access Denied',
                icon: 'pi pi-fw pi-lock',
                routerLink: ['/auth/access'],
              },
            ],
          },
          {
            label: 'Danh sách quyền',
            icon: 'pi pi-fw pi-clone',
            routerLink: ['/system/role'],
            permission: 'AbpIdentity.Roles',
          },
          {
            label: 'Danh sách người dùng',
            icon: 'pi pi-fw pi-clone',
            routerLink: ['/system/user'],
            permission: 'AbpIdentity.Users',
          },
        ],
      },
      {
        label: 'Get Started',
        items: [
          {
            label: 'Documentation',
            icon: 'pi pi-fw pi-question',
            routerLink: ['/documentation'],
          },
          // {
          //     label: 'View Source', icon: 'pi pi-fw pi-search', url: ['https://github.com/primefaces/sakai-ng'], target: '_blank'
          // }
        ],
      },
    ];
  }
}
