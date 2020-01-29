import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { SidebarTogglerDirective } from './sidebar.toggler';
import { MobileSidebarTogglerDirective } from './mobile.sidebar.toggler';
import { AsideMenuTogglerDirective } from './aside.menu.toggler';

@NgModule({
  imports: [CommonModule],
  declarations: [
    HeaderComponent,
    SidebarTogglerDirective,
    MobileSidebarTogglerDirective,
    AsideMenuTogglerDirective
  ],
  exports: [
    HeaderComponent,
    SidebarTogglerDirective,
    MobileSidebarTogglerDirective,
    AsideMenuTogglerDirective
  ]
})
export class HeaderModule {}
