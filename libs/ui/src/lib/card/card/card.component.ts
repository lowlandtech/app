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
import { DynamicOverlay } from '@lowlandtech/shared';
import { CloseableComponent } from '../closeable';
import { CardFacade } from '../+state';
import { CardStateData } from '../+state';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
/* #endregion */

@Component({
  selector: 'scx-ui-card, [component="scx-ui-card"]',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  @HostBinding('class.card--expanded') class2 = false;

  @Input() cardId: string;
  @Input() hasHeader = true;
  @Input() hasSettings = false;
  @Input() collapsable = true;
  @Input() closeable = true;
  @Input() hasFooter = false;
  @Input() cancelable = true;
  @Input() okable = true;
  @Input() expandable = true;

  @Output() normalizing: EventEmitter<string> = new EventEmitter();
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
  public minimized: boolean;
  public expanded: boolean;
  public card: CardStateData;
  constructor(
    private facade: CardFacade,
    private dynamicOverlay: DynamicOverlay,
    private elRef: ElementRef
  ) {}

  ngOnInit(): void {
    this.facade.selectCard(this.cardId);
    this.facade.getCard(this.cardId).subscribe(card => {
      this.card = card;
      this.class2 = this.card.expanded;
    });
  }

  public onCollapsing(){
    this.facade.collapse(this.cardId);
    this.collapsing.emit(this.cardId);
  }

  public onExpanding(){
    this.facade.expand(this.cardId);
    this.expanding.emit(this.cardId);
  }

  public onHiding(){
    this.facade.hide(this.cardId);
    this.hiding.emit(this.cardId);
  }

  public onClosing() {
    this.dynamicOverlay.setContainerElement(this.elRef.nativeElement);
    const overlayRef = this.dynamicOverlay.create({
      positionStrategy: this.dynamicOverlay.position().global().centerHorizontally().centerVertically(),
      hasBackdrop: true
    });
    this.overlay = new ComponentPortal(CloseableComponent);
    const componentRef = overlayRef.attach(this.overlay);
    componentRef.instance.closing.subscribe(() => {
      this.closing.emit(this.cardId);
    });
    componentRef.instance.cancelling.subscribe(() => {
      overlayRef.dispose();
    });
    componentRef.changeDetectorRef.detectChanges();
  }
}
