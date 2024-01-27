import LoadingComponent from "../../app/layout/LoadingComponent";
import { UseAppSelector, useAppDispatch } from "../../app/store/configureStore";
import ProductList from "./ProductList";
import { useEffect } from "react";
import { fetchProductsAsync, productSelectors } from "./catalogSlice";

export default function Catalog() {
  const products = UseAppSelector(productSelectors.selectAll);
  const {productsLoaded, status} = UseAppSelector(state => state.catalog);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!productsLoaded) dispatch(fetchProductsAsync());
  }, [productsLoaded, dispatch])

  if (status.includes('pending')) return <LoadingComponent message='Loading products...' />

  return(
    <>
      <ProductList products={products} />
    </>
  )
}
