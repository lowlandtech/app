import { Directive, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { AdminStateFacade } from '../../+state';

@Directive({
  selector: '[scxBrandMinimizer]'
})
export class BrandMinimizerDirective implements OnInit {
  private className = 'brand-minimized';
  private element: HTMLBodyElement;

  constructor(
    @Inject(DOCUMENT)
    private document: Document,
    private facade: AdminStateFacade){
    this.element = this.document.querySelector('body');
  }

  public ngOnInit() {
    this.facade.sidebar$.subscribe(sidebar => {
      if(sidebar.state === 'MINIMIZED') {
        this.element.classList.add(this.className);
      } else {
        this.element.classList.remove(this.className);
      }
    });
  }
}
