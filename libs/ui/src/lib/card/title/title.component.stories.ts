
import { UiModule } from '../../ui.module';
import { TitleComponent } from './title.component';

export default {
  title: 'TitleComponent'
}

export const primary = () => ({
  moduleMetadata: {
    imports: []
  },
  component: TitleComponent,
  props: {
  }
})
