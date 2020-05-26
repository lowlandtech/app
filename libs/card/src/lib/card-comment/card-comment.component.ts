import {
  Component,
  Input,
  ChangeDetectionStrategy,
  EventEmitter,
  Output
} from '@angular/core';
import { CardComment, CardData, User } from '@lowlandtech/api';

@Component({
  selector: 'scx-card-comment',
  templateUrl: './card-comment.component.html',
  styleUrls: ['./card-comment.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CardCommentComponent {
  @Input() currentUser: User;
  @Input() comment: CardComment;
  @Input() card: CardData;
  @Output() delete: EventEmitter<{
    commentId: number;
    slug: string;
  }> = new EventEmitter();
}
