import { Directive, HostListener } from '@angular/core';

/**
* Allows the sidebar to be toggled via click.
*/
@Directive({
  selector: '[scxSidebarToggler]'
})
export class SidebarTogglerDirective {
  constructor() { }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    document.querySelector('body').classList.toggle('sidebar-hidden');
  }
}
