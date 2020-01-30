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
import { BrandMinimizerDirective } from './sidebar-minimizer';
import { SidebarNavItemComponent } from './sidebar-nav';
import { SidebarNavDropdownComponent } from './sidebar-nav';
import { SidebarNavLinkComponent } from './sidebar-nav';
import { SidebarNavTitleComponent } from './sidebar-nav';
import { MainComponent } from './main';
import { SidebarCloseDirective } from './sidebar-close';
import { NavDropdownDirective } from './nav-dropdown';
import { NavDropdownToggleDirective } from './nav-dropdown';

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
    BrandMinimizerDirective,
    SidebarNavItemComponent,
    SidebarNavDropdownComponent,
    SidebarNavLinkComponent,
    SidebarNavTitleComponent,
    MainComponent,
    SidebarCloseDirective,
    NavDropdownDirective,
    NavDropdownToggleDirective
  ],
  exports: [
    SidebarComponent,
    SidebarFooterComponent,
    SidebarHeaderComponent,
    SidebarFormComponent,
    SidebarMinimizerComponent,
    SidebarNavComponent,
    SidebarMinimizerDirective,
    BrandMinimizerDirective,
    SidebarNavItemComponent,
    SidebarNavDropdownComponent,
    SidebarNavLinkComponent,
    SidebarNavTitleComponent,
    MainComponent,
    SidebarCloseDirective,
    NavDropdownDirective,
    NavDropdownToggleDirective
  ]
})
export class SidebarModule {}
