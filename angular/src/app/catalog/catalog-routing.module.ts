import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionGuard } from '@abp/ng.core';
import { FormulaCategoryComponent } from './formulaCategory/formula-categorycomponent';
import { MaterialCategoryComponent } from './materialCategory/material-categorycomponent';
import { ToolCategoryComponent } from './toolCategory/tool-categorycomponent';

const routes: Routes = [
  {
    path: 'formula-category',
    component: FormulaCategoryComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'HCNAdminCatalog.FormulaCategory',
    },
  },
  {
    path: 'material-category',
    component: MaterialCategoryComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'HCNAdminCatalog.MaterialCategory',
    },
  },
  {
    path: 'tool-category',
    component: ToolCategoryComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'HCNAdminCatalog.ToolCategory',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
