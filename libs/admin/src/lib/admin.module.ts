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
import { ToolbarsComponent } from './toolbar';
import { StoreModule } from '@ngrx/store';
import {
  ADMINSTATE_FEATURE_KEY,
  adminStateReducer,
  initialAdminState,
  metaReducers,
  AdminStateFacade
} from './+state';
import { AsideListItemComponent } from './aside';
import { AsideListComponent } from './aside';
import { SharedModule } from '@lowlandtech/shared';
import { AsideListGroupDirective } from './aside';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    StoreModule.forRoot(adminStateReducer, {
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true
      }
    }),
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
    ToolbarsComponent,
    AsideListItemComponent,
    AsideListComponent,
    AsideListGroupDirective
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
    ToolbarsComponent,
    AsideListItemComponent,
    AsideListComponent
  ],
  entryComponents: [
    AsideListItemComponent
  ],
  providers: [
    AdminStateFacade
  ]
})
export class AdminModule {}
