import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

/* #region card components */
import {
  CardComponent,
  HeaderComponent,
  FooterComponent,
  BodyComponent,
  ToolbarComponent,
  TitleComponent
} from './card';

const card = [
  CardComponent,
  HeaderComponent,
  FooterComponent,
  BodyComponent,
  ToolbarComponent,
  TitleComponent
];
/* #endregion */

@NgModule({
  imports: [CommonModule],
  declarations: [
    ...card
  ],
  exports: [
    ...card
  ]
})
export class UiModule {}
