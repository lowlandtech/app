import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { PlaygroundComponent } from './playground.component';

export const playgroundRoutes: Route[] = [];

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [PlaygroundComponent],
  exports: [PlaygroundComponent]
})
export class PlaygroundModule {}
