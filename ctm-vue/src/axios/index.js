import axios from "axios";
import { success, failure } from "../service/HttpResponse.js";

const MISAAxios = axios.create({
  baseURL: "https://localhost:7015/api/v1/",
  headers: { Accept: "application/json", "Content-Type": "application/json" },
});

MISAAxios.interceptors.response.use(
  function (response) {
    return success(response);
  },
  function (error) {
    return failure(error.response);
  }
);

export default MISAAxios;
