import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from './card/card.module';


@NgModule({
  imports: [
    CommonModule,
    CardModule
  ],
  exports:[
    CardModule,
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class UiModule {}
