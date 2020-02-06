import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PlaygroundComponent } from './playground.component';

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
  ],
  declarations: [PlaygroundComponent],
  exports: [PlaygroundComponent]
})
export class PlaygroundModule {}
