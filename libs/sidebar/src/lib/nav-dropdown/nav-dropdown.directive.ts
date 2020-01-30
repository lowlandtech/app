import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[scxNavDropdown]'
})
export class NavDropdownDirective {

  constructor(private el: ElementRef) { }

  toggle() {
    this.el.nativeElement.classList.toggle('open');
  }

}
