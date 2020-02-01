import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import jss from 'jss';
import preset from 'jss-preset-default';

jss.setup(preset());

@NgModule({
  imports: [CommonModule]
})
export class ThemeModule { }
