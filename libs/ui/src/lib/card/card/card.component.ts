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
import { CardStatus } from '@spotacard/api';
import { CloseableComponent } from '../closeable';
import { CardFacade } from '../+state';
import { CardStateData } from '../+state';
/* #endregion */

@Component({
  selector: 'scx-ui-card, [component="scx-ui-card"]',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  @HostBinding('class.card--expanded')
  @Input() isExpanded = false;

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
        } else {
          this.card = {
            id: this.cardId,
            status: CardStatus.NORMAL
          }
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
    console.log('click')
    if(this.card.status === CardStatus.EXPANDED){
      this.card.status = CardStatus.NORMAL;
      this.facade.normalize(this.card);
      this.normalizing.emit(this.cardId);
      this.isExpanded = false;
    } else if(this.card.status === CardStatus.NORMAL){
      this.card.status = CardStatus.EXPANDED;
      this.facade.expand(this.card);
      this.expanding.emit(this.cardId);
      this.isExpanded = true;
    }
  }

  public onHiding(){
    if(this.card.status === CardStatus.HIDDEN){
      this.card.status = CardStatus.NORMAL;
      this.facade.normalize(this.card);
      this.normalizing.emit(this.cardId);
    } else if(this.card.status === CardStatus.NORMAL){
      this.card.status = CardStatus.HIDDEN;
      this.facade.hide(this.card);
      this.hiding.emit(this.cardId);
    }
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
