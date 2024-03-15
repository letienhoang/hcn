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
import { FormulaCategoryComponent } from './formulaCategory/formula-category.component';
import { FormulaCategoryDetailComponent } from './formulaCategory/formula-category-detail.component';
import { MaterialCategoryComponent } from './materialCategory/material-category.component';
import { MaterialCategoryDetailComponent } from './materialCategory/material-category-detail.component';
import { ToolCategoryComponent } from './toolCategory/tool-category.component';
import { ToolCategoryDetailComponent } from './toolCategory/tool-category-detail.component';
import { TopicComponent } from './topic/topic.component';
import { TopicDetailComponent } from './topic/topic-detail.component';
import { StoryComponent } from './story/story.component';
import { StoryDetailComponent } from './story/story-detail.component';
import { StoryTagComponent } from './story/story-tag.component';
import { TagComponent } from './tag/tag.component';
import { TagDetailComponent } from './tag/tag-detail.component';
import {MultiSelectModule} from 'primeng/multiselect';
import {ChipsModule} from 'primeng/chips';

@NgModule({
  declarations: [
    FormulaCategoryComponent,
    FormulaCategoryDetailComponent,
    MaterialCategoryComponent,
    MaterialCategoryDetailComponent,
    ToolCategoryComponent,
    ToolCategoryDetailComponent,
    TopicComponent,
    TopicDetailComponent,
    StoryComponent,
    StoryDetailComponent,
    StoryTagComponent,
    TagComponent,
    TagDetailComponent
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
    MultiSelectModule,
    ChipsModule
  ],
  entryComponents: [
    FormulaCategoryDetailComponent,
    MaterialCategoryDetailComponent,
    ToolCategoryDetailComponent,
    TopicDetailComponent,
    StoryDetailComponent,
    StoryTagComponent,
    TagDetailComponent
  ],
})
export class CatalogModule {}
