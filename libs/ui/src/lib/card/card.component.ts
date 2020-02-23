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
import { CardFacade } from './+state/card.facade';
import { CardStateData } from './+state/card.models';
import { CardStatus } from '@spotacard/api';
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
  public card: CardStateData;

  constructor(
    private facade: CardFacade,
    private dynamicOverlay: DynamicOverlay,
    private elRef: ElementRef
  ) {}

  ngOnInit(): void {
    if(!this.card||!this.cardId) {
      this.facade.cards$.subscribe(cards => {
        const card = cards.find(_card => _card.id === this.cardId)
        if(card){
          this.card = card;
          console.log('found:'+ this.card.id);
        } else {
          this.card = {
            id: this.cardId,
            status: CardStatus.NORMAL
          }
          console.log('not:found:'+ this.card.id);
        }
      })
    }
  }

  public onCollapsing(){
    if(this.card.status === CardStatus.MINIMIZED){
      this.card.status = CardStatus.NORMAL;
      this.facade.normalize(this.card);
      this.normalizing.emit(this.cardId);
    } else if(this.card.status === CardStatus.NORMAL){
      this.card.status = CardStatus.MINIMIZED;
      this.facade.collapse(this.card);
      this.collapsing.emit(this.cardId);
    }
  }

  public onExpanding(){
    this.card.status = this.card.status === CardStatus.EXPANDED
        ? CardStatus.NORMAL : CardStatus.EXPANDED;
    this.facade.expand(this.card);
    this.expanding.emit(this.card.id);
  }

  public onHiding(){
    this.card.status = this.card.status === CardStatus.HIDDEN
        ? CardStatus.NORMAL : CardStatus.HIDDEN;
    this.facade.hide(this.card);
    this.hiding.emit(this.card.id);
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
