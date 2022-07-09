import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Layout } from "./components/Layout";

import "./App.less";
import { MediaList } from "./pages/MediasList";
import { Settings } from "./pages/Settings";
import { DragUpload } from "./pages/Upload";

export default function App() {
  return (
    <BrowserRouter>
      {/* <ConfigProvider prefixCls="light"> */}
      <Layout>
        <Routes>
          <Route element={<MediaList />} index />
          <Route
            caseSensitive={false}
            path="/upload"
            element={<DragUpload />}
          />
          <Route caseSensitive={false} path="/setting" element={<Settings />} />
        </Routes>
      </Layout>
      {/* </ConfigProvider> */}
    </BrowserRouter>
  );
}
