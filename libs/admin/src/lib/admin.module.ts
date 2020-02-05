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
import { ContentComponent } from './content';
import { AsideComponent, AsideTogglerDirective } from './aside';
import { BreadcrumbsComponent } from './breadcrumbs';
import { StoreModule } from '@ngrx/store';
import {
  ADMINSTATE_FEATURE_KEY,
  adminStateReducer,
  initialAdminState,
  metaReducers,
  AdminStateFacade
} from './+state';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    StoreModule.forFeature(ADMINSTATE_FEATURE_KEY, adminStateReducer, {
      initialState: initialAdminState,
      metaReducers: metaReducers
    }),
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
  ],
  providers: [
    AdminStateFacade
  ]
})
export class AdminModule {}
