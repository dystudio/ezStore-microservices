// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { WarehouseComponent } from './warehouse.component';
import { WarehouseHttpService } from '../../../core/services/warehouse.service';
import { SettingRoutingModule } from './setting-routing.module';
import { WarehouseDetailComponent } from './warehouse-detail.component';
import { NgxLoadingModule } from 'ngx-loading';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SettingRoutingModule,
    NgxLoadingModule.forRoot({}),
  ],
  providers: [
    WarehouseHttpService
  ],
  declarations: [
    WarehouseComponent,
    WarehouseDetailComponent
  ]
})
export class SettingModule { }
