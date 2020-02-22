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
import { CloseableComponent } from './closeable';
/* #endregion */

@Component({
  selector: 'scx-ui-card, [component="scx-ui-card"]',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  @Input() cardId: string;
  @Input() hasHeader = true;
  @Input() hasSettings = false;
  @Input() collapsable = true;
  @Input() closeable = true;
  @Input() hasFooter = false;
  @Input() cancelable = true;
  @Input() okable = true;

  @Output() hiding: EventEmitter<string> = new EventEmitter();
  @Output() removing: EventEmitter<string> = new EventEmitter();
  @Output() collapsing: EventEmitter<string> = new EventEmitter();
  @Output() expanding: EventEmitter<string> = new EventEmitter();
  @Output() editing: EventEmitter<string> = new EventEmitter();
  @Output() appending: EventEmitter<string> = new EventEmitter();
  @Output() prepending: EventEmitter<string> = new EventEmitter();
  @Output() closing: EventEmitter<string> = new EventEmitter();
  @Output() cancelling: EventEmitter<string> = new EventEmitter();
  @Output() oking: EventEmitter<string> = new EventEmitter();

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
      this.oking.emit(this.cardId);
    });
    componentRef.instance.cancelling.subscribe(() => {
      overlayRef.dispose();
    });
    componentRef.changeDetectorRef.detectChanges();
  }
}
