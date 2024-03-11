import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { HolwnSharedModule } from '../shared/modules/holwn-shared.module';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CalendarModule } from 'primeng/calendar';
import { CatalogRoutingModule } from './catalog-routing.module';
import { FormulaCategoryComponent } from './formulaCategory/formula-categorycomponent';
import { FormulaCategoryDetailComponent } from './formulaCategory/formula-category-detail.component';
import { MaterialCategoryComponent } from './materialCategory/material-categorycomponent';
import { MaterialCategoryDetailComponent } from './materialCategory/material-category-detail.component';
import { ToolCategoryComponent } from './toolCategory/tool-categorycomponent';
import { ToolCategoryDetailComponent } from './toolCategory/tool-category-detail.component';
@NgModule({
  declarations: [
    FormulaCategoryComponent,
    FormulaCategoryDetailComponent,
    MaterialCategoryComponent,
    MaterialCategoryDetailComponent,
    ToolCategoryComponent,
    ToolCategoryDetailComponent
  ],
  imports: [
    SharedModule,
    CatalogRoutingModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    HolwnSharedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    CalendarModule,
  ],
  entryComponents: [
    FormulaCategoryDetailComponent,
    MaterialCategoryDetailComponent,
    ToolCategoryDetailComponent
  ],
})
export class CatalogModule {}
