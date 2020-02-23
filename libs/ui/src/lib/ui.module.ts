import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from './card';

@NgModule({
  imports: [
    CommonModule,
    CardModule
  ],
  exports:[
    CardModule,
  ]
})
export class UiModule {}
