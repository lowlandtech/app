
import { UiModule } from '../../ui.module';
import { ToolbarComponent } from './toolbar.component';

export default {
  title: 'ToolbarComponent'
}

export const primary = () => ({
  moduleMetadata: {
    imports: []
  },
  component: ToolbarComponent,
  props: {
  }
})
