import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { SidebarTogglerDirective } from './sidebar.toggler';
import { MobileSidebarTogglerDirective } from './mobile.sidebar.toggler';

@NgModule({
  imports: [CommonModule],
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
