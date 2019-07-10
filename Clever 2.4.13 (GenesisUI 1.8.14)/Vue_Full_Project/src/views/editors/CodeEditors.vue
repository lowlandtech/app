<template>
  <div class="animated fadeIn">
    <b-card show-footer>
      <div slot="header">
        CodeMirror
        <div class="card-actions"><a href="https://github.com/surmon-china/vue-codemirror" target="_blank">docs</a></div>
      </div>
      <codemirror v-model="code" :options="editorOption"></codemirror>
      <div slot="footer">
        <b-form inline>
          <!--<b-form-fieldset>-->
          <b-form-group>
            <b-form-select :plain="true" size="sm" name="selectedTheme" v-model="selectedTheme" @change.native="onThemeChange($event)">
              <option value="material">material</option>
              <option value="eclipse">eclipse</option>
            </b-form-select>
          </b-form-group>
          <b-form-group>
            <b-form-select :plain="true" size="sm" name="selectedMode" v-model="selectedMode" @change.native="onModeChange($event)">
              <option value="markdown">markdown</option>
              <option value="javascript">javascript</option>
              <option value="xml">html</option>
            </b-form-select>
          </b-form-group>
        <!--</b-form-fieldset>-->
        </b-form>
      </div>
  </b-card>
</div>
</template>


<script>
import Vue from 'vue'
import VueCodeMirror from 'vue-codemirror'
// require styles
import 'codemirror/lib/codemirror.css'
import 'codemirror/theme/material.css'
import 'codemirror/theme/eclipse.css'
import 'codemirror/mode/markdown/markdown'
import 'codemirror/mode/javascript/javascript'
import 'codemirror/mode/xml/xml'

// global use
Vue.use(VueCodeMirror)

const defaults = {
  markdown: '### [CodeMirror](http://codemirror.net)\n' +
  'A versatile _text_ editor implemented in **JavaScript*** for the browser. \n' +
  'It is specialized for editing `code`, and comes with a number of language modes and addons that implement more advanced editing functionality.',
  javascript: 'var component = {\n\tname: "vue-codemirror",\n\tauthor: "Surmon",\n\trepo: "https://github.com/surmon-china/vue-codemirror"\n};',
  xml: '<h1>I ♥ vue-codemirror</h1>'
}

export default {
  name: 'code-editors',
  data () {
    return {
      code: defaults.markdown,
      editorOption: {
        tabSize: 4,
        styleActiveLine: true,
        lineNumbers: true,
        lineWrapping: true,
        line: true,
        mode: 'markdown',
        theme: 'material'
      },
      selectedMode: 'markdown',
      selectedTheme: 'material'
    }
  },
  methods: {
    onThemeChange (e) {
      this.editorOption.theme = e.target.value
    },
    onModeChange (e) {
      this.editorOption.mode = e.target.value
      this.code = defaults[this.editorOption.mode]
    }
  },
  mounted () {
    setTimeout(() => {
      this.code += '\n\n\t~haha~'
    }, 2000)
  }
}
</script>
