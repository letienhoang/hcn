import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionGuard } from '@abp/ng.core';
import { FormulaCategoryComponent } from './formulaCategory/formula-categorycomponent';

const routes: Routes = [
  {
    path: 'formula-category',
    component: FormulaCategoryComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'HCNAdminCatalog.FormulaCategory',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
