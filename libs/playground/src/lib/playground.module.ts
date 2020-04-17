import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PlaygroundComponent } from './playground.component';
import { UiModule } from '@lowlandtech/ui';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        pathMatch: 'full',
        component: PlaygroundComponent
      },
    ]),
    UiModule
  ],
  declarations: [PlaygroundComponent],
  exports: [PlaygroundComponent]
})
export class PlaygroundModule {}
