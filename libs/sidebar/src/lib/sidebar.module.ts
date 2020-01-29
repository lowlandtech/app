import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { SidebarComponent } from './sidebar.component';
import { SidebarFooterComponent } from './sidebar-footer';
import { SidebarHeaderComponent } from './sidebar-header/';
import { SidebarFormComponent } from './sidebar-form';
import { SidebarMinimizerComponent } from './sidebar-minimizer';
import { SidebarNavComponent } from './sidebar-nav';
import { SidebarMinimizerDirective } from './sidebar-minimizer';
import { BrandMinimizerDirective } from './sidebar-minimizer/brand-minimizer.directive';

export const sidebarRoutes: Route[] = [];

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [
    SidebarComponent,
    SidebarFooterComponent,
    SidebarHeaderComponent,
    SidebarFormComponent,
    SidebarMinimizerComponent,
    SidebarNavComponent,
    SidebarMinimizerDirective,
    BrandMinimizerDirective
  ],
  exports: [
    SidebarComponent,
    SidebarFooterComponent,
    SidebarHeaderComponent,
    SidebarFormComponent,
    SidebarMinimizerComponent,
    SidebarNavComponent,
    SidebarMinimizerDirective,
    BrandMinimizerDirective
  ]
})
export class SidebarModule {}
