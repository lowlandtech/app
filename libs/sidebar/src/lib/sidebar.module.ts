import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { SidebarComponent } from './sidebar.component';
import { SidebarFooterComponent } from './sidebar-footer';
import { SidebarHeaderComponent } from './sidebar-header/';
import { SidebarFormComponent } from './sidebar-form';
import { SidebarMinimizerComponent } from './sidebar-minimizer';
import { SidebarNavComponent } from './sidebar-nav';

export const sidebarRoutes: Route[] = [];

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [SidebarComponent, SidebarFooterComponent, SidebarHeaderComponent, SidebarFormComponent, SidebarMinimizerComponent, SidebarNavComponent],
  exports: [SidebarComponent, SidebarFooterComponent, SidebarHeaderComponent, SidebarFormComponent, SidebarMinimizerComponent, SidebarNavComponent]
})
export class SidebarModule {}
