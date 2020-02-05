import { Directive, HostListener } from '@angular/core';
import { NavDropdownDirective } from './nav-dropdown.directive';

@Directive({
  selector: '[scxNavDropdownToggle]'
})
export class NavDropdownToggleDirective {
  constructor(private dropdown: NavDropdownDirective) {}

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    this.dropdown.toggle();
  }
}
