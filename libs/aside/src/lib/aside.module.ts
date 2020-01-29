import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AsideComponent } from './aside.component';

@NgModule({
  imports: [CommonModule],
  declarations: [AsideComponent],
  exports: [AsideComponent]
})
export class AsideModule {}
