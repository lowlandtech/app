/* #region imports */
import {
  Component,
  OnInit,
  HostBinding,
  Input,
  Output,
  EventEmitter,
  ElementRef
} from '@angular/core';
import { ComponentPortal } from '@angular/cdk/portal';
import { DynamicOverlay } from '@spotacard/shared';
import { Card } from './card.model';
import { CloseableComponent } from './closeable';
/* #endregion */

@Component({
  selector: 'scx-ui-card, [component="scx-ui-card"]',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  @Input() card: Card;
  @Input() hasHeader = true;
  @Input() hasSettings = false;
  @Input() sizeable = true;
  @Input() closeable = true;
  @Input() hasFooter = false;
  @Input() cancelable = true;
  @Input() okable = true;

  @Output() hide: EventEmitter<Card> = new EventEmitter();
  @Output() remove: EventEmitter<Card> = new EventEmitter();
  @Output() collapse: EventEmitter<Card> = new EventEmitter();
  @Output() expand: EventEmitter<Card> = new EventEmitter();
  @Output() edit: EventEmitter<Card> = new EventEmitter();
  @Output() append: EventEmitter<Card> = new EventEmitter();
  @Output() prepend: EventEmitter<Card> = new EventEmitter();

  private overlay: ComponentPortal<CloseableComponent>;

  constructor(
    private dynamicOverlay: DynamicOverlay,
    private elRef: ElementRef
  ) {}

  ngOnInit(): void {
  }

  public onClose() {
    this.dynamicOverlay.setContainerElement(this.elRef.nativeElement);
    const overlayRef = this.dynamicOverlay.create({
      positionStrategy: this.dynamicOverlay.position().global().centerHorizontally().centerVertically(),
      hasBackdrop: true
    });
    this.overlay = new ComponentPortal(CloseableComponent);
    const componentRef = overlayRef.attach(this.overlay);
    componentRef.instance.closing.subscribe(() => {

    });
    componentRef.instance.cancelling.subscribe(() => {
      overlayRef.dispose();
    });
    componentRef.changeDetectorRef.detectChanges();
  }
}
