import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { SidebarComponent } from './sidebar.component';
import { SidebarFooterComponent } from './sidebar-footer/sidebar-footer.component';
import { SidebarHeaderComponent } from './sidebar-header/sidebar-header.component';
import { SidebarFormComponent } from './sidebar-form/sidebar-form.component';

export const sidebarRoutes: Route[] = [];

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [SidebarComponent, SidebarFooterComponent, SidebarHeaderComponent, SidebarFormComponent],
  exports: [SidebarComponent, SidebarFooterComponent, SidebarHeaderComponent, SidebarFormComponent]
})
export class SidebarModule {}
