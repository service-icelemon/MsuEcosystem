import React from "react";
import { Editor, EditorState, RichUtils } from "draft-js";

function DraftEditor() {
  const [editorState, setEditorState] = React.useState(
    EditorState.createEmpty()
  );

  return <div></div>;
}

export default DraftEditor;
