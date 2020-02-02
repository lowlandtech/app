import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { SidebarTogglerDirective } from './sidebar.toggler';
import { MobileSidebarTogglerDirective } from './mobile.sidebar.toggler';
import { AsideModule } from '@spotacard/aside';

@NgModule({
  imports: [
    CommonModule,
    AsideModule
  ],
  declarations: [
    HeaderComponent,
    SidebarTogglerDirective,
    MobileSidebarTogglerDirective
  ],
  exports: [
    HeaderComponent,
    SidebarTogglerDirective,
    MobileSidebarTogglerDirective
  ]
})
export class HeaderModule {}
