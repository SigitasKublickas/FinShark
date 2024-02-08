import axios from "axios";
import { CompanySearch } from "./company";

interface SeacrhResponse {
  data: CompanySearch[];
}

export const searchCompanies = async (query: string) => {
  try {
    const data = await axios.get<SeacrhResponse>(`
        https://financialmodelingprep.com/api/v3/search-ticker?query=${query}&limit=10&exchange=NASDAQ&apikey=${process.env.REACT_APP_API_KEY}
        `);
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("error", error.message);
      return error.message;
    } else {
      console.log("unexpected", error);
      return "Unexpected";
    }
  }
};
