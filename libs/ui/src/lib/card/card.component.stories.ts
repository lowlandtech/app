import { text, number, boolean } from '@storybook/addon-knobs';
import { UiModule } from '../ui.module';
import { CardComponent } from './card.component';

export default {
  title: 'CardComponent'
}

export const primary = () => ({
  moduleMetadata: {
    imports: []
  },
  component: CardComponent,
  props: {
    card: {
      id: '1',
      title: 'card 1',
      key: 'card-1',
      type: 'task',
      tags: ['tag1','tag2']
    },
  }
})
