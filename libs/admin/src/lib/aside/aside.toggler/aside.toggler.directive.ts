import { Directive, HostListener, ElementRef, HostBinding, Inject, OnInit } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { AdminStateFacade } from '../../+state';
import { hasClass } from '../../tools';

/**
* Allows the aside to be toggled via click.
*/
@Directive({
  selector: '[scxAsideToggler]'
})
export class AsideTogglerDirective implements OnInit {
  @HostBinding('class.d-md-down-none') class1 = true;
  private className = 'aside-menu-hidden';
  private element: HTMLBodyElement;

  constructor(
    @Inject(DOCUMENT)
    private document: Document,
    private facade: AdminStateFacade,
    private elementRef:ElementRef){
    this.elementRef.nativeElement.innerHTML ='<span class="navbar-toggler-icon"></span>';
    this.element = this.document.querySelector('body');
  }

  public ngOnInit() {
    this.facade.aside$.subscribe(aside => {
      if (aside.hidden) {
        this.element.classList.add(this.className);
      } else {
        this.element.classList.remove(this.className);
      }
    });
  }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    if(!hasClass(this.element, this.className)) {
      this.facade.asideHide();
    } else {
      this.facade.asideShow();
    }
  }
}

