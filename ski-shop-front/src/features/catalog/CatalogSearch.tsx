import { TextField, debounce } from "@mui/material";
import { UseAppSelector } from "../../app/store/configureStore";
import { useDispatch } from "react-redux";
import { setProductParams } from "./catalogSlice";
import { useState } from "react";

export default function ProductSearch() {
  const { productParams } = UseAppSelector(state => state.catalog);
  const [searchTerm, setSearchTerm] = useState(productParams.searchTerm);
  const dispatch = useDispatch();

  const debouncedSearch = debounce((event: any) => {
      dispatch(setProductParams({ searchTerm: event.target.value }))
  }, 1000);

  return (
      <TextField
          label='Search products'
          variant='outlined'
          fullWidth
          value={searchTerm || ''}
          onChange={(event: any) => {
              setSearchTerm(event.target.value);
              debouncedSearch(event);
          }}
      />
  )
}