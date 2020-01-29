import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { SidebarTogglerDirective } from './sidebar.toggler';

@NgModule({
  imports: [CommonModule],
  declarations: [HeaderComponent, SidebarTogglerDirective],
  exports: [HeaderComponent, SidebarTogglerDirective]
})
export class HeaderModule {}
