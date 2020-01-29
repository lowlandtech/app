import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { SidebarComponent } from './sidebar.component';

export const sidebarRoutes: Route[] = [];

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [SidebarComponent],
  exports: [SidebarComponent]
})
export class SidebarModule {}
