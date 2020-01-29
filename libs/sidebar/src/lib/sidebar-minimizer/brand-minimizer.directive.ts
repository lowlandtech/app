import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[scxBrandMinimizer]'
})
export class BrandMinimizerDirective {

  constructor() { console.log('BrandMinimizerDirective'); }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    document.querySelector('body').classList.toggle('brand-minimized');
  }
}
