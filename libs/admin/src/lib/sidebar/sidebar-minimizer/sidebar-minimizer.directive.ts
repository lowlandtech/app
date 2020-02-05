import { Directive, HostListener, OnInit, ElementRef, Inject } from '@angular/core';
import { AdminStateFacade } from '../../+state';
import { DOCUMENT } from '@angular/common';
import { hasClass } from '../../tools';

@Directive({
  selector: '[scxSidebarMinimizer]'
})
export class SidebarMinimizerDirective implements OnInit {
  private className = 'sidebar-minimized';
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

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    if(hasClass(this.element, this.className)) {
      this.facade.maximize();
    } else {
      this.facade.minimize();
    }
  }

}
