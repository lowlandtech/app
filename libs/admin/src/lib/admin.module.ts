import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import {
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
  NavDropdownToggleDirective,
  SidebarComponent
} from './sidebar';
import {
  HeaderComponent,
  SidebarTogglerDirective,
  MobileSidebarTogglerDirective
} from './header';
import { FooterComponent } from './footer';
import { ContentComponent } from './content/content.component';
import { AsideComponent, AsideTogglerDirective } from './aside';
import { BreadcrumbsComponent } from './breadcrumbs';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
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
    NavDropdownToggleDirective,
    HeaderComponent,
    SidebarTogglerDirective,
    MobileSidebarTogglerDirective,
    FooterComponent,
    ContentComponent,
    AsideComponent,
    AsideTogglerDirective,
    BreadcrumbsComponent
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
    NavDropdownToggleDirective,
    HeaderComponent,
    SidebarTogglerDirective,
    MobileSidebarTogglerDirective,
    FooterComponent,
    ContentComponent,
    AsideComponent,
    AsideTogglerDirective,
    BreadcrumbsComponent
  ]
})
export class AdminModule {}
